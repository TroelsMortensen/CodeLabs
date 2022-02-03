var currentTab = 0; // Current tab is set to be the first tab (0)
//showTab(currentTab); // Display the current tab

function showFirstOrSpecificTab() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const slide = urlParams.get('slide')

    if (slide == "" || slide == null) {
        showTab(currentTab);
        return;
    }
    var slideIdx = parseInt(slide);
    if (!isNaN(slideIdx)) {
        currentTab = slideIdx - 1;
    }
    showTab(currentTab);
}

function showTab(n) {

    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    if (x.length == 0) {

    }
    x[n].style.display = "block";

    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
        document.getElementById("prevBtn1").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
        document.getElementById("prevBtn1").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Done";
        document.getElementById("nextBtn1").innerHTML = "Done";
    } else {
        document.getElementById("nextBtn").innerHTML = "Next >";
        document.getElementById("nextBtn1").innerHTML = "Next >";
    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)
}

function setTab(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = n;

    // Otherwise, display the correct tab:
    showTab(currentTab);
    updateLineHighlight();
}

function nextPrev(n) {
    window.scrollTo(0, 0);
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // starting over
        window.location.href = "/CodeLabs/index.html";
        // currentTab = 0;
        // setTab(0);
    } else {
        // Otherwise, display the correct tab:
        showTab(currentTab);
        updateLineHighlight();
    }
}

// Everything below is stolen from prism.js, and it is used to add line-highlighting to code blocks. No idea how.

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}

function c(e) {
    return !(!e || !/pre/i.test(e.nodeName)) && (!!e.hasAttribute("data-line") || !(!e.id || !Prism.util.isActive(e, s)))
}

function b(e) {
    e()
}

function v(e, t) {
    return Array.prototype.slice.call((t || document).querySelectorAll(e))
}

function updateLineHighlight() {
    v("pre").filter(c).map(function (e) {
        console.log("updating");
        return Prism.plugins.lineHighlight.highlightLines(e)
    }).forEach(b)
}

