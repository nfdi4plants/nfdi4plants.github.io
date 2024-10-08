Promise.all([
  d3.json("/data/nfdimap/germany.json"),
  d3.buffer("/data/nfdimap/participantsM.xlsx")
]).then(([germany, buffer]) => {

  const tmp = XLSX.read(buffer, { type: 'array'});

  return {
    people: XLSX.utils.sheet_to_json(tmp.Sheets.Peoples),
    projects: XLSX.utils.sheet_to_json(tmp.Sheets.project),
    places: XLSX.utils.sheet_to_json(tmp.Sheets.Places),
    germany: germany
  }
}).then(data => {

  // Utility: format a person's name with their role
  formatName = function(d) {
    return d.Name + (d.Role ? ` (${d.Role})` : '');
  };

  // Utility: make an HTML list with a heading (for People and Projects)
  makeList = function(heading, entries) {
    if (entries.length === 0) return ''; // Do not show if no entries
    return `<h4>${heading}</h4>` +
           '<ul>' +
           entries.map(e => `<li>${e}</li>`).join('') +
           '</ul>';
  };

  // Combine data for each institution (people + projects)
  function combineInstitutionData(institution, people, projects) {
    const peopleList = people.map(formatName);
    const projectList = projects.map(p => p.Project);

    // Show the institution name as a header (h4), and then lists of people and projects
    const combinedEntries = [
      `<h3>${institution}</h3>`,  // Display institution name as a header
      makeList('People', peopleList),
      makeList('Projects', projectList)  // Only show Projects if they exist
    ];

    return combinedEntries.join('');
  }

  // Function to determine the class attributes for a given place based on people
  function placeClass(entries) {
    const allRoles = String(entries.map(d => d.Role).join('')).toLowerCase();
    return allRoles.includes('spokesperson') ? 'applicant' : 'normal';
  }

  // Pre-process places
  const projection = d3.geoConicConformal().center([20.22, 49.3]).scale(2750);
  const placeIndex = d3.index(data.places, d => d.Name);

  const places = d3.groups(
    data.people,
    p => p.City,
    ).map(([place, people]) => {
      // Group projects by institution as well
      const projectsByInstitution = d3.group(data.projects, p => p.Institution);

      // Group people by institution
      const peopleByInstitution = d3.group(people, p => p.Institution);

      return {
        name: place,
        info: [...peopleByInstitution.entries()].map(([inst, people]) => {
          const projects = projectsByInstitution.get(inst) || []; // Get projects for this institution, or empty array if none
          return combineInstitutionData(inst, people, projects); // Combine people and projects per institution
        }).join(''),
        coord: (d => projection([d.Longitude, d.Latitude]))(placeIndex.get(place)),
        type: placeClass(people)  // determine class based on people roles
      };
    });

  const path = d3.geoPath().projection(projection);

  // Begin map drawing
  const svg = d3
    .select("#dataplant-map")
    .append("svg")
    .attr("viewBox", [0, 0, 300, 400]);

  // Draw base map
  svg
    .append("g")
    .selectAll("path")
    .data(topojson.feature(data.germany, data.germany.objects.states).features)
    .join("path")
    .attr("d", path)
    .classed("map", true);

  // Draw places
  const placesnodes = svg
    .append("g")
    .selectAll("circle")
    .data(places)
    .join("circle")
    .classed("place", true)
    .attr("cx", d => d.coord[0])
    .attr("cy", d => d.coord[1])
    .attr("r", () => 4)
    .attr('data-tippy-content', d => d.info)
    .attr('data-place-type', d => d.type);

  // Add tooltips using tippy.js
  tippy(placesnodes.nodes(), {
    appendTo: (reference) => document.querySelector('#dataplant-map'),
    interactive: true,
    maxWidth: 'none',
    allowHTML: true,
    theme: 'custom',
    delay: [100, 100]
  });
});

