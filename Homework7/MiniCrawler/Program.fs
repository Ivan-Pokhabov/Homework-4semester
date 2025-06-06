module MiniCrawler

open System
open System.Net.Http
open System.Text.RegularExpressions

type CrawlerResult = {
    Url: string
    Size: int
    IsSuccess: bool
}

let private hrefPattern = @"href=[""'](https://[^""']+)[""']"

let private downloadContentAsync (client: HttpClient) (url: string) = 
    async {
        try
            let uri = Uri(url)
            let! content = client.GetStringAsync(uri) |> Async.AwaitTask
            return Some content
        with ex ->
            printfn "Error downloading %s: %s" url ex.Message
            return None
    }

let private extractLinks content =
    Regex.Matches(content, hrefPattern)
    |> Seq.cast<Match>
    |> Seq.choose (fun m -> 
        if m.Groups.Count > 1 then Some m.Groups[1].Value else None)
    |> Seq.distinct
    |> Seq.toArray

let private getPageSize (client: HttpClient) (url: string) =
    async {
        match! downloadContentAsync client url with
        | Some content ->
            return { Url = url; Size = content.Length; IsSuccess = true }
        | None ->
            return { Url = url; Size = 0; IsSuccess = false }
    }

let crawl (client: HttpClient) rootUrl  =
    async {
        let! mainContent = downloadContentAsync client rootUrl
        
        let! results = 
            match mainContent with
            | Some content ->
                content
                |> extractLinks
                |> Array.map (getPageSize client)
                |> Async.Parallel
            | None -> 
                async { return [||] }

        return results |> Array.sortByDescending (fun x -> x.Size)
    }