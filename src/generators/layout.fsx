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
#load "../loaders/jobsloader.fsx"
#load "../loaders/serviceloader.fsx"
#endif

open Html
open Globals.HTMLComponents

[<Literal>]
let baseUrl = "https://github.com/nfdi4plants/nfdi4plants.github.io/tree/main/src/"

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
    
let layout (ctx : SiteContents) (activePageTitle: string) bodyCnt =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    let ttl =
        siteInfo
        |> Option.map (fun si ->
            if activePageTitle <> "" then
                si.title + " - " + activePageTitle
            else
                si.title
        )
        |> Option.defaultValue ""

    html [Class "has-navbar-fixed-top"; HtmlProperties.Style [CSSProperties.Custom("scroll-behavior", "smooth")]] [
        head [] [
            meta [CharSet "utf-8"]
            meta [Name "viewport"; Content "width=device-width, initial-scale=1"]
            link [Rel "icon"; Type "image/png"; Sizes "32x32"; Href "/images/favicon.png"]

            title [] [!! ttl]


            link [Rel "stylesheet"; Href "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"]

            link [Rel "stylesheet"; Type "text/css"; Href ("/style/style.css")]
            link [Rel "stylesheet"; Type "text/css"; Href "https://cdn.jsdelivr.net/npm/@creativebulma/bulma-collapsible@1.0.4/dist/css/bulma-collapsible.min.css"]
            
            script [ Defer true; Src "https://kit.fontawesome.com/0d3e0ea7a6.js"; CrossOrigin "anonymous"] []
            script [ Type "module"; Src "/js/bundle.js"] []
            style [] [
                !! """
                    body {
                        margin: 0px;
                    }

                    thead tr th, strong {
                        color: var(--accent-text-color) !important
                    }

                    a {
                        color: var(--link-color, #4FB3D9) !important;
                    }

                    a:hover {
                        color: var(--link-hover-color, #3A3A3A) !important;
                    }

                    thead {
                        font-size: 1.2rem;
                    }

                    nfdi-toc, nfdi-body {
                        --outside-background-color: rgb(240, 243, 246);
                        --header-color: rgb(10, 12, 16)
                        --element-text-color: #0E1116;
                        --element-background-color: #fff;
                        --accent-text-color: rgb(31, 194, 167);
                        --link-color: #4FB3D9;
                        --link-hover-color: #8ad3ee;
                    }
                """
            ]
        ]
        custom "nfdi-navbar" [] []
        body [] [
            yield! bodyCnt
        ]
        custom "nfdi-footer" [] []          
    ]

let render (ctx : SiteContents) cnt =
    cnt
    |> HtmlElement.ToString
    #if WATCH
    |> injectWebsocketCode 
    #endif
