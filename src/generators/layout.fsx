#r "../_lib/Fornax.Core.dll"
#load "../globals.fsx"
#if !FORNAX
#load "../loaders/pageloader.fsx"
#load "../loaders/globalloader.fsx"
#load "../loaders/newsloader.fsx"
#load "../loaders/mainpagecardloader.fsx"
#load "../loaders/learnmoreloader.fsx"
#load "../loaders/aboutloader.fsx"
#load "../loaders/imprintloader.fsx"
#load "../loaders/privacyloader.fsx"
#endif

open Html
open Globals.HTMLComponents

let injectWebsocketCode (webpage:string) =
    let websocketScript =
        """
        <script type="text/javascript">
          var wsUri = "ws://localhost:8080/websocket";
      function init()
      {
        websocket = new WebSocket(wsUri);
        websocket.onclose = function(evt) { onClose(evt) };
      }
      function onClose(evt)
      {
        console.log('closing');
        websocket.close();
        document.location.reload();
      }
      window.addEventListener("load", init, false);
      </script>
        """
    let head = "<head>"
    let index = webpage.IndexOf head
    webpage.Insert ( (index + head.Length + 1),websocketScript)
    
let layout (ctx : SiteContents) active bodyCnt =
    let pages = ctx.TryGetValues<Pageloader.Page> () |> Option.defaultValue Seq.empty
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let ttl =
        siteInfo
        |> Option.map (fun si -> si.title)
        |> Option.defaultValue ""

    let menuEntries =
        pages
        |> Seq.map (fun p ->
            let cls = if p.title = active then "navbar-item is-active smooth-hover" else "navbar-item"
            a [Class cls; Href p.link] [!! p.title ]
        )
        |> Seq.toList

    html [Class "has-navbar-fixed-top"] [
        head [] [
            meta [CharSet "utf-8"]
            meta [Name "viewport"; Content "width=device-width, initial-scale=1"]
            link [Rel "icon"; Type "image/png"; Sizes "32x32"; Href "/images/favicon.png"]

            title [] [!! ttl]


            link [Rel "stylesheet"; Href "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"]

            link [Rel "stylesheet"; Type "text/css"; Href ("/style/style.css")]
            link [Rel "stylesheet"; Type "text/css"; Href "https://cdn.jsdelivr.net/npm/@creativebulma/bulma-collapsible@1.0.4/dist/css/bulma-collapsible.min.css"]
            
            script [ Defer true; Src "https://kit.fontawesome.com/0d3e0ea7a6.js"; CrossOrigin "anonymous"][]
            script [ Src "/js/navbar.js"][]
        ]
        body [] [
            nav [Class "navbar is-fixed-top has-bg-darkblue"] [
                div [Class "navbar-brand"] [
                    a [Class "navbar-item"; Href "/"] [
                        img [Src ("/images/logo.svg"); Alt "Logo"; Width "32"; Height "32"]
                    ]
                    a [
                        Class "navbar-burger"; 
                        Custom ("data-target", "navMenu"); 
                        Custom ("aria-label", "menu"); 
                        HtmlProperties.Role "button"
                        Custom ("aria-expanded", "false")
                    ] [
                        span [HtmlProperties.Custom ("aria-hidden","true")] []
                        span [HtmlProperties.Custom ("aria-hidden","true")] []
                        span [HtmlProperties.Custom ("aria-hidden","true")] []
                    ]
                ]
                div [Id "navMenu"; Class "navbar-menu has-bg-darkblue"] [
                    div [Class "navbar-start is-justify-content-center is-flex-grow-1"] menuEntries
                    div [Class "navbar-end"] [
                        a [Class "navbar-item"; Href "https://twitter.com/nfdi4plants"] [icon "fab fa-twitter"]
                        a [Class "navbar-item"; Href "https://github.com/nfdi4plants"] [icon "fab fa-github"]
                        a [Class "navbar-item"; Href "https://www.youtube.com/channel/UCrTBwQWOa0-aWYkwoY104Wg"] [icon "fab fa-youtube"]
                        a [Class "navbar-item"; Href "mailto:dataplant@uni-kl.de"] [icon "fas fa-paper-plane"]
                        a [Class "navbar-item"; Href "https://zenodo.org/communities/nfdi4plants?page=1&size=20"; HtmlProperties.Style [FontWeight "bold"]] [!! "Z"]
                    
                    ]
                ]
            ]
            yield! bodyCnt
        ]
        footer [Class "footer has-bg-darkblue-lighter-20"] [ 
            div [Class "container"] [
                div [Class "columns"] [
                    div [Class "column is-4 m-4"] [
                        block [ h3 [Class "subtitle is-white"] [!!"DataPLANT - Democratization of plant research made easy."]]
                        block [ p [] [!!"DataPLANT is part of "; a [Href "https://www.nfdi.de/"] [!!"NFDI"]]]
                        block [ p [] [!!"This website is developed and maintained by members of DataPLANT"]]
                    ]
                    div [Class "column is-4 m-4"] [
                        block [ h3 [Class "subtitle is-white"] [!!"Navigation"]]
                        ul [] [
                            block [li [] [a [Href "/"] [!!"Home"]]]
                            block [li [] [a [Href "/news.html"] [!!"News"]]]
                            block [li [] [a [Href "/content/learn-more/service.html"] [!!"Service"]]]
                            block [li [] [a [Href "/content/about.html"] [!!"About"]]]
                        ]
                    ]
                    div [Class "column is-4 m-4"] [
                        block [ h3 [Class "subtitle is-white"] [!!"Social"]]
                        block [ whiteIcon "fab fa-twitter"; a [Href "https://twitter.com/nfdi4plants"] [!!"DataPLANT on Twitter"]]
                        block [ whiteIcon "fab fa-github"; a [Href "https://github.com/nfdi4plants"] [!!"DataPLANT open source projects on GitHub"]]
                        block [ whiteIcon "fab fa-youtube"; a [Href "https://www.youtube.com/channel/UCrTBwQWOa0-aWYkwoY104Wg"] [!!"DataPLANT video resources on youtube"]]
                        block [ h3 [Class "subtitle is-white"] [!!"Legal"]]
                        block [ a [Href "/content/imprint.html"] [!!"Imprint"]]
                        block [ a [Href "/content/privacy.html"] [!!"Privacy"]]
                    ]
                ]
            ]
        ]
            
    ]

let render (ctx : SiteContents) cnt =
    cnt
    |> HtmlElement.ToString
    #if WATCH
    |> injectWebsocketCode 
    #endif
