#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"
#load "../globals.fsx"

open Globals
open Globals.HTMLComponents
open Html

let createImageComponent ratio src =
    figure [Class (sprintf "image is-%s" ratio)] [
        img [Src src]
    ]
    
let createCard bgColor cardBorder children =

    let cardBG = Colors.hasBgColor bgColor
    let cardBorder = Colors.hasBorderColor cardBorder

    section [Class (sprintf "section")] [
        div [Class (sprintf "container box %s %s" cardBG cardBorder)] [
            div [Class "container details-card"] [
                yield! children
            ]
        ]
    ]

let generate' (ctx : SiteContents) (_: string) =
    let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
    
    Layout.layout ctx "" [

        section [Class "section"] [
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "lightblue")
                h1 [Class (sprintf "title %s" emphasisColor)] [!!"Creating ARCs using the ARC Commander"]
                div [Class "columns"] [
                    div [Class "column is-4 is-hidden-mobile"] [
                        figure [Class "image"] [
                            img [Src "/images/details/Real-world-ARC2.svg"]
                        ]
                    ]
                    div [Class "column is-8"] [
                        p [] [!!"After this basic usage instruction, you will know​:"]
                        ul [] [
                            li [] [p [] [!! "- the general command line structure​"]]
                            li [] [p [] [!! "- the most important commands to create and validate your ARCs.​"]]
                        ]
                        p [] [!!"To find the latest release of the ARC Commander, please click "; a [Href "https://github.com/nfdi4plants/arcCommander/releases"] [!!"here"]]
                        p [] [!!"For further Information visit the "; a [Href "https://github.com/nfdi4plants/arcCommander"] [!!"ARC Commander Github Page"]]
                        p [] [!!"Although using the Commander will support you immensely in the creation of ARCs, it is optional and the ISA-TAB excel sheets can still be filled out manually."]
                    ]
                ]
            ]
            createCard "mint" "mint" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"General command line structure"]
                    code [Class (sprintf "%s code-example" emphasisColor)] [!!"arc &lt;object&gt; &lt;subcommand-verb&gt; &lt;subcommand-verb-args&gt;"]
                    !!"where "
                    i [Class emphasisColor] [!!"object"]
                    !!"is either:​"
                    ul [] [
                        li [] [
                            p [] [em [Class emphasisColor] [
                                !!"- &lt;investigation&gt; or its sub-objects​"
                            ]]
                        ]
                        li [] [
                            p [] [em [Class emphasisColor] [!!"- &lt;study&gt; or its sub-objects​"]]
                        ]
                        li [] [
                            p [] [em [Class emphasisColor] [!!"- &lt;assay&gt;"]]
                        ]
                        li [] [
                           p [] [em [Class emphasisColor] [!!"- &lt;configuration&gt;"]]
                        ]
                    ]
                    p [] [!!"and "]
                    i [Class emphasisColor] [!!"""subcommand-verb"""]
                    !!"models what to do with the object.​"
                ]
            ]
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Initialize ARC​"]
                    p [] [!!"Navigate to a folder in which you want to initialize an ARC. Open the a Command Prompt (via typing search for 'cmd' on windows or 'Terminal on MacOS') in the folder address and press Enter. Use"]
                    code [Class "code-example"] [!!"arc init"]
                    p [] [!! "to create the basic "; em [Class emphasisColor] [!!"ARC folder structure."]]
                ]
            ]
            createCard "lightblue" "lightblue" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Creating investigations"]
                    p [] [!!"Each ARC includes an isa.investigation file that serves as central information registry."]
                    p [] [!!"To generate such a file use"]
                    code [Class "code-example"] [!!"arc i create"]
                    p [] [!!"This will open an editor asking for basic investigation information to create an ISA-TAB conform Excel file. Each investigation file MUST have a unique Identifier."]
                ]
            ]
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Registering ARC​ objects"]
                    p [] [!!"To register persons, publications, factors, protocols, … use  arc &lt;object&gt; register:"]
                    code [Class "code-example"] [!!"arc investigation (i) person register"]
                    code [Class "code-example"] [!!"arc i publication register"]
                    code [Class "code-example"] [!!"arc study (s) factor register"]
                    code [Class "code-example"] [!!"arc s protocol register"]
                    p [] [!! "This transports information directly into the isa.investigation.xlsx file"]
                ]
            ]
            createCard "darkblue" "darkblue" [
                let emphasisColor = (Colors.isColor "lightblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Adding objects to an ARC​"]
                    code [Class "code-example"] [!!"arc &lt;object&gt; add"]
                    p [] [
                        !!"The command arc add lets you combine"
                        em [] [!!"arc init and arc register."] 
                        !!"It will create a new &lt;object&gt; in the ARC and subsequently register it with the passed arguments."]
                ]
            ]
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Removing objects from an ARC"]
                    code [Class "code-example"] [!!"arc &lt;object&gt; remove"]
                    p [] [!! "The command arc remove results in the exact opposite! It lets you combine arc unregister and arc delete, deleting the &lt;object&gt; from the ARC file structure and the ARC’s registry."]
                ]
            ]
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Initialize ARC​"]
                    p [] [!!"Navigate to a folder in which you want to initialize an ARC. Open the a Command Prompt (via typing search for 'cmd' on windows or 'Terminal on MacOS') in the folder address and press Enter. Use"]
                    code [Class "code-example"] [!!"arc init"]
                    p [] [!! "to create the basic "; em [Class emphasisColor] [!!"ARC folder structure."]]
                ]
            ]
            createCard "lightblue" "lightblue" [
                let emphasisColor = (Colors.isColor "white")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Editing and updating ARC​ objects"]
                    p [] [!!"Edit &lt;objects&gt; using"]
                    code [Class "code-example"] [!!"arc &lt;object&gt; &lt;sub-object&gt; edit"]
                    p [] [
                        !! "e.g." 
                        em [Class emphasisColor] [!!"arc i person edit"]
                        !! "Without further arguments an editor opens that asks for the person that should be edited. If the person exists, another editor opens to edit the information."
                    ]
                    br []
                    code [Class "code-example"] [!!"arc &lt;object&gt; &lt;sub-object&gt; update"]
                    p [] [
                        !! "To avoid editor pop ups, also the command " 
                        code [Class "code-example"] [!!"arc &lt;object&gt; &lt;sub-object&gt; update"]
                        !! "can be used, e. g. "
                        em [Class emphasisColor] [!!"arc i person edit"]
                        code [Class "code-example"] [!!"arc i person update –l Doe –f John"]
                        code [Class "code-example"] [!!"--address 'Musterstraße 1, 12345 Musterstadt'"]
                    ]
                ]
            ]
            createCard "white" "white" [
                let emphasisColor = (Colors.isColor "darkblue")
                div [Class "card-container"] [
                    h1 [Class (sprintf "title %s" emphasisColor)] [!!"Listing ARC​ contents"]
                    p [] [!!"To list registered &lt;objects&gt;, use"]
                    code [Class "code-example"] [!!"arc &lt;object&gt; list"]
                    p [] [
                        !! "This will show you registered"
                        em [Class emphasisColor] [!!"persons, publications, assays, … "]
                        !!"of an investigation or a study. For a detailed usage instruction, please click here."
                    ]
                ]
            ]
        ]
    ]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
    generate' ctx page
    |> Layout.render ctx