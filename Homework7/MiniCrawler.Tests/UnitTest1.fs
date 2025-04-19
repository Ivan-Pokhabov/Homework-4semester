module MiniCrawler.Tests

open FsUnit
open System.Net
open System.Net.Http
open System.Threading
open System.Threading.Tasks
open NUnit.Framework
open MiniCrawler

type MockHttpMessageHandler(responseMap: Map<string, string>) =
    inherit HttpMessageHandler()

    override _.SendAsync(request: HttpRequestMessage, cancellationToken: CancellationToken) =
        let rawUrl = request.RequestUri.ToString()
        let normalizedUrl = if rawUrl.EndsWith "/" then rawUrl.TrimEnd '/' else rawUrl
        
        let contentOption =
            match responseMap.TryFind rawUrl with
            | Some c -> Some c
            | None -> responseMap.TryFind normalizedUrl

        match contentOption with
        | Some content ->
            let resp = new HttpResponseMessage(HttpStatusCode.OK)
            resp.Content <- new StringContent(content)
            Task.FromResult(resp)
        | None ->
            let resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            Task.FromResult(resp)

[<TestFixture>]
type ``MiniCrawler Tests`` () =

    [<Test>]
    member _.``crawl returns sorted page sizes for linked pages`` () =
        let rootHtml = """
            <html>
                <body>
                    <a href="https://example.com/page1">Люблю F#</a>
                    <a href="https://example.com/page2">Не люблю диффуры</a>
                </body>
            </html>
        """

        let responseMap =
            Map [
                "https://example.com", rootHtml
                "https://example.com/", rootHtml
                "https://example.com/page1", "Математика -- зло."
                "https://example.com/page2", "Сейчас бы холодного пи***."
            ]

        let handler = new MockHttpMessageHandler(responseMap)
        let client = new HttpClient(handler)


        let result = crawl client "https://example.com" |> Async.RunSynchronously


        Assert.AreEqual(2, result.Length)
        Assert.Greater(result.[0].Size, result.[1].Size)
        Assert.AreEqual("https://example.com/page2", result.[0].Url)
        Assert.That(result.[0].IsSuccess)
        Assert.That(result.[1].IsSuccess)

    [<Test>]
    member _.``crawl handles missing pages gracefully`` () =
        let rootHtml = """
            <html>
                <body>
                    <a href="https://example.com/page1">Лол</a>
                    <a href="https://example.com/missing">О нет невалидная ссылка</a>
                </body>
            </html>
        """

        let responseMap =
            Map [
                "https://example.com", rootHtml
                "https://example.com/", rootHtml
                "https://example.com/page1", "Some actual content here."
            ]

        let handler = new MockHttpMessageHandler(responseMap)
        let client = new HttpClient(handler)


        let result = crawl client "https://example.com" |> Async.RunSynchronously

        let page1 = result |> Array.find (fun r -> r.Url = "https://example.com/page1")
        let missing = result |> Array.find (fun r -> r.Url = "https://example.com/missing")


        Assert.AreEqual(2, result.Length)

        Assert.That page1.IsSuccess
        Assert.AreEqual("Some actual content here.".Length, page1.Size)

        Assert.That(not missing.IsSuccess)
        Assert.AreEqual(0, missing.Size)
