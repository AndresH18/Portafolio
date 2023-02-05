import {Project} from "./shared.js";


const projects = new Set([
    new Project("One Punch", "API", "C#", "Asp.Net Core", "<i>REST</i> API sobre <b>One Punch Man</b>.", "https://github.com/AndresH18/OnePunch"),
    new Project("One Punch", "API", "C#", "Asp.Net Core", "<i>REST</i> API sobre <b>One Punch Man</b>.", "https://github.com/AndresH18/OnePunch"),
    new Project("One Punch", "API", "C#", "Asp.Net Core", "<i>REST</i> API sobre <b>One Punch Man</b>.", "https://github.com/AndresH18/OnePunch"),
    new Project("One Punch", "API", "C#", "Asp.Net Core", "<i>REST</i> API sobre <b>One Punch Man</b>.", "https://github.com/AndresH18/OnePunch"),
    new Project("One Punch", "API", "C#", "Asp.Net Core", "<i>REST</i> API sobre <b>One Punch Man</b>.", "https://github.com/AndresH18/OnePunch"),
])

window.onload = () => {
    // add active class to nav element
    let activeNavElement = document.getElementById("nav-projects");
    activeNavElement.classList.add("active")

    let siteLinks = document.getElementsByClassName("site-link")
    for (const link of siteLinks) {
        link.target = "_blank"
        link.rel = "noreferrer noopener"
    }

    loadProjects()

    console.log("Loaded")
}

function loadProjects() {
    const projectsTable = document.getElementById("projects-table")

    // TODO: use api call, use for-of loop
    projects.forEach(function (key, val) {
        let row = projectsTable.insertRow()

        row.insertCell(0).innerHTML = val.name
        row.insertCell(1).innerHTML = val.type
        row.insertCell(2).innerHTML = val.language
        row.insertCell(3).innerHTML = val.framework
        row.insertCell(4).innerHTML = val.description
        row.insertCell(5).innerHTML = `<a class="btn btn-dark" href="${val.url}">Repositorio</a>`
    })

}