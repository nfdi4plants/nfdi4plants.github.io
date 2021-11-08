---
title: DataPLANT Participants
bg-color: white
border-color: white
emphasis-color: mint-darker-40
image:
layout: text-only
index: 1

---

Already in the application phase, some research groups from the field of plant research decided to collaborate with DataPLANT. Now we are working to successively identify and cover the needs of the entire research landscape.  

You are interested in finding out who is already involved? Please take a look at the map below.

If you would prefer to get all the names at a glance, please have a look at the [GEPRIS entry](https://gepris.dfg.de/gepris/projekt/442077441 "GEPRIS: DataPLANT-Daten in Pflanzen-Grundlagenforschung") or the [proposal](https://zenodo.org/record/4836395#.YYlE6WDMLZs "Proposal: DataPLANT - NFDI Consortium for Fundamental Plant Research") of DataPLANT.

<style type="text/css">
    a {
            color: #C2E5CD;
        }

    #dataplant-map {
        margin: 0 auto;
        overflow: visible;
        max-width: 500px;
    }

    #dataplant-map > svg .map {
        fill: #C2E5CD;
        stroke: #FFFFFF;
        stroke-width: 1px;
        vector-effect: non-scaling-stroke;
    }

    #dataplant-map > svg .place {
        fill: #137464;
    }

    #dataplant-map > svg .place[data-place-type=applicant] {
        fill: #1FC2A7;
    }
    .tippy-box[data-theme~="custom"] {
        background-color: #137464;
        color: white;
        border: 1px solid #FFFFFF;
        border-radius: 15;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }    
</style>
<div>
<div id="dataplant-map"></div>
</div>

<script src="https://unpkg.com/d3@7.1.1"></script>
<script src="https://unpkg.com/xlsx@0.17.3"></script>
<script src="https://unpkg.com/topojson-client@3"></script>
<script src="https://unpkg.com/@popperjs/core@2"></script>
<script src="https://unpkg.com/tippy.js@6"></script>
<script src="/js/nfdimap.js"></script>