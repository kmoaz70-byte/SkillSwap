// ============ SkillSwap landing page interactions ============

document.addEventListener("DOMContentLoaded", function () {
    var nav = document.getElementById("mainNav");

    // Add shadow to navbar once the page is scrolled
    function updateNavShadow() {
        if (window.scrollY > 8) {
            nav.classList.add("scrolled");
        } else {
            nav.classList.remove("scrolled");
        }
    }
    updateNavShadow();
    window.addEventListener("scroll", updateNavShadow, { passive: true });

    // Close the mobile menu automatically after a nav link is tapped
    var navMenu = document.getElementById("navMenu");
    var navLinks = navMenu ? navMenu.querySelectorAll(".nav-link, .btn-gradient") : [];
    navLinks.forEach(function (link) {
        link.addEventListener("click", function () {
            if (navMenu.classList.contains("show") && window.bootstrap) {
                var collapseInstance = bootstrap.Collapse.getOrCreateInstance(navMenu);
                collapseInstance.hide();
            }
        });
    });

    // Gentle fade/slide-in reveal for sections as they enter the viewport
    var revealEls = document.querySelectorAll("[data-reveal]");
    if ("IntersectionObserver" in window && revealEls.length) {
        var observer = new IntersectionObserver(
            function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add("in-view");
                        observer.unobserve(entry.target);
                    }
                });
            },
            { threshold: 0.15 }
        );
        revealEls.forEach(function (el) { observer.observe(el); });
    } else {
        // Fallback: just show everything if IntersectionObserver isn't supported
        revealEls.forEach(function (el) { el.classList.add("in-view"); });
    }
});
