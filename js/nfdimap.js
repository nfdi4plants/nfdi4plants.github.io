Promise.all([
  d3.json("/data/nfdimap/germany.json"), 
  d3.buffer("/data/nfdimap/participants2.xlsx")
]).then( ([germany, buffer]) => {

  const tmp = XLSX.read(buffer, { type: 'array'})

  return {
    people:  XLSX.utils.sheet_to_json(tmp.Sheets.People),
    places:  XLSX.utils.sheet_to_json(tmp.Sheets.Places),
    germany: germany
  }
}).then( data => {

  // determine how to show a person's name
  formatName = function(d) {
    return d.Name + (d.Role ? ` (${d.Role})` : '')
  }

  // utility: make a HTML list with a heading
  makeList = function(heading, entries) {
    return `<h4>${heading}</h4>` + 
            '<ul>' + 
            entries.map(e => `<li>${e}</li>`).join('') + 
          '</ul>'
  }

  // determine the class attributes for a given place from its persons
  placeClass = function(entries) {
    const allRoles = String(entries.map(d => d.Role).join('')).toLowerCase();

    // simple logic for now
    if( allRoles.includes('spokesperson') )
      return 'applicant'

    return 'normal'
  }

  // pre-process places
  const projection = d3.geoConicConformal().center([20.22, 49.3]).scale(2750);
  const placeIndex = d3.index(data.places, d => d.Name)
  
  const places = d3.groups(
    data.people, 
    p => p.City, 
    ).map( ([place, people]) => ({
      name:  place,
      info:  d3.groups( people, p => p.Institution )
      .map( ([inst, people]) => {
        return makeList( inst, people.map(formatName) )
      })
      .join(''),
      coord: (d => projection([d.Longitude, d.Latitude]))(placeIndex.get(place)),
      type:  placeClass(people)
    }))
    
  const path = d3.geoPath().projection(projection);
    
  // begin map drawing
  const svg = d3
    .select("#dataplant-map")
    .append("svg")
    .attr("viewBox", [0, 0, 300, 400]);

  // draw base map
  svg
    .append("g")
    .selectAll("path")
    .data(topojson.feature(data.germany, data.germany.objects.states).features)
    .join("path")
    .attr("d", path)
    .classed("map", true);

  const placesnodes = svg
    .append("g")
    .selectAll("circle")
    .data(places)
    .join("circle")
    .classed("place", true)
    .attr("cx", d => d.coord[0])
    .attr("cy", d => d.coord[1])
    .attr("r", () => 4)
    .attr('data-tippy-content', d=> d.info)
    .attr('data-place-type', d => d.type)

  tippy(placesnodes.nodes(), {
    appendTo: (reference) => document.querySelector('#dataplant-map'),
    interactive: true,
    maxWidth: 'none',
    allowHTML: true,
    theme: 'custom',
    delay: [100, 1500]
    })
})
