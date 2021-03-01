#r "../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../loaders/pageloader.fsx"
#load "../loaders/globalloader.fsx"
#endif

open Html

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
            nav [Class "navbar is-fixed-top"] [
                div [Class "navbar-brand"] [
                    a [Class "navbar-item"; Href "/"] [
                        img [Class "is-32x32"; Src ("/images/logo.svg"); Alt "Logo"]
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
                div [Id "navMenu"; Class "navbar-menu"] menuEntries
            ]
            yield! bodyCnt
        ]
        footer [Class "footer"] [ 

        ]
            
    ]

let render (ctx : SiteContents) cnt =
    cnt
    |> HtmlElement.ToString
    #if WATCH
    |> injectWebsocketCode 
    #endif
