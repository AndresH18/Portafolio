window.onload = () => {
    // add active class to nav element
    let activeNavElement = document.getElementById("nav-about");
    activeNavElement.classList.add("active")

    let siteLinks = document.getElementsByClassName("site-link")
    for (const link of siteLinks) {
        link.target = "_blank"
        link.rel = "noreferrer noopener"
    }

    console.log("Loaded")
}
