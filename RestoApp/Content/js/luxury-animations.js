(function () {
    "use strict";

    var reduceMotion = window.matchMedia && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    function ready(callback) {
        if (document.readyState === "loading") {
            document.addEventListener("DOMContentLoaded", callback, { once: true });
        } else {
            callback();
        }
    }

    function forEach(selector, callback) {
        Array.prototype.forEach.call(document.querySelectorAll(selector), callback);
    }

    function markReveal(element, index, direction) {
        if (!element || element.classList.contains("lux-reveal")) {
            return;
        }

        element.classList.add("lux-reveal");
        element.classList.add(direction || "lux-reveal-up");
        element.style.setProperty("--lux-delay", Math.min(index * 70, 280) + "ms");

        if (!element.hasAttribute("data-aos")) {
            element.setAttribute("data-aos", direction === "lux-reveal-down" ? "fade-down" : "fade-up");
            element.setAttribute("data-aos-duration", "720");
            element.setAttribute("data-aos-easing", "ease-out-cubic");
            element.setAttribute("data-aos-delay", Math.min(index * 60, 240).toString());
        }
    }

    function applyRevealSystem() {
        var groups = [
            [".hero-section .container, .menu-header .container, main > .row:first-child .col-md-12", "lux-reveal-down"],
            [".row.mb-4.align-items-center, .d-flex.justify-content-between.align-items-center.mb-4", "lux-reveal-down"],
            [".card, .table-responsive, .alert, .pagination, .empty-state, [id$='pnlNoResults'], [id$='pnlPagination']", "lux-reveal-up"],
            [".form-control, .form-select, .btn", "lux-reveal-up"]
        ];

        groups.forEach(function (group) {
            forEach(group[0], function (element, index) {
                markReveal(element, index % 5, group[1]);
            });
        });

        if (reduceMotion) {
            forEach(".lux-reveal", function (element) {
                element.classList.add("is-visible");
            });
            return;
        }

        if ("IntersectionObserver" in window) {
            var observer = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add("is-visible");
                        observer.unobserve(entry.target);
                    }
                });
            }, {
                root: null,
                threshold: 0.12,
                rootMargin: "0px 0px -42px 0px"
            });

            forEach(".lux-reveal", function (element) {
                observer.observe(element);
            });
        } else {
            forEach(".lux-reveal", function (element) {
                element.classList.add("is-visible");
            });
        }

        requestAnimationFrame(function () {
            forEach(".hero-section .lux-reveal, .menu-header .lux-reveal", function (element) {
                element.classList.add("is-visible");
            });
        });

        if (window.AOS) {
            window.AOS.init({
                once: true,
                mirror: false,
                offset: 72,
                duration: 720,
                easing: "ease-out-cubic",
                disable: reduceMotion
            });
        }
    }

    function addMagneticHover() {
        if (reduceMotion || !window.matchMedia || !window.matchMedia("(pointer: fine)").matches) {
            return;
        }

        forEach(".card, .btn, .page-link", function (element) {
            element.addEventListener("pointermove", function (event) {
                var rect = element.getBoundingClientRect();
                var x = ((event.clientX - rect.left) / rect.width - 0.5) * 2;
                var y = ((event.clientY - rect.top) / rect.height - 0.5) * 2;

                element.style.setProperty("--tilt-x", Math.max(Math.min(-y * 3.5, 3.5), -3.5).toFixed(2) + "deg");
                element.style.setProperty("--tilt-y", Math.max(Math.min(x * 3.5, 3.5), -3.5).toFixed(2) + "deg");
            });

            element.addEventListener("pointerleave", function () {
                element.style.removeProperty("--tilt-x");
                element.style.removeProperty("--tilt-y");
            });
        });
    }

    function createAmbientCanvas() {
        if (reduceMotion || document.querySelector(".luxury-canvas")) {
            return;
        }

        var canvas = document.createElement("canvas");
        var context = canvas.getContext("2d");
        var width = 0;
        var height = 0;
        var phase = 0;

        canvas.className = "luxury-canvas";
        canvas.setAttribute("aria-hidden", "true");
        document.body.insertBefore(canvas, document.body.firstChild);

        function resize() {
            var ratio = Math.min(window.devicePixelRatio || 1, 2);
            width = window.innerWidth;
            height = window.innerHeight;
            canvas.width = Math.floor(width * ratio);
            canvas.height = Math.floor(height * ratio);
            context.setTransform(ratio, 0, 0, ratio, 0, 0);
        }

        function ribbon(offset, color, alpha, amplitude, speed) {
            context.beginPath();
            context.lineWidth = 1.2;
            context.strokeStyle = color.replace("ALPHA", alpha);

            for (var x = -80; x <= width + 80; x += 24) {
                var progress = x / Math.max(width, 1);
                var y = height * offset +
                    Math.sin(progress * Math.PI * 2.2 + phase * speed) * amplitude +
                    Math.cos(progress * Math.PI * 4.6 + phase * 0.65) * (amplitude * 0.34);

                if (x === -80) {
                    context.moveTo(x, y);
                } else {
                    context.lineTo(x, y);
                }
            }

            context.stroke();
        }

        function draw() {
            phase += 0.01;
            context.clearRect(0, 0, width, height);

            var veil = context.createLinearGradient(0, 0, width, height);
            veil.addColorStop(0, "rgba(255, 250, 243, 0.55)");
            veil.addColorStop(0.48, "rgba(243, 238, 228, 0.24)");
            veil.addColorStop(1, "rgba(49, 85, 79, 0.10)");
            context.fillStyle = veil;
            context.fillRect(0, 0, width, height);

            ribbon(0.24, "rgba(200, 155, 72, ALPHA)", 0.30, 18, 1.1);
            ribbon(0.48, "rgba(159, 84, 61, ALPHA)", 0.18, 28, 0.9);
            ribbon(0.72, "rgba(49, 85, 79, ALPHA)", 0.22, 22, 1.25);

            window.requestAnimationFrame(draw);
        }

        window.addEventListener("resize", resize);
        resize();
        draw();
    }

    function applyImageFallbacks() {
        function replaceBrokenImage(image) {
            if (image.dataset.luxFallbackApplied === "true") {
                return;
            }

            var wrapper = document.createElement("div");
            var label = document.createElement("span");
            var icon = document.createElement("i");
            var alt = image.getAttribute("alt") || "Restaurant photo";

            wrapper.className = "lux-img-fallback";
            icon.className = "fa-solid fa-utensils";
            label.textContent = alt;

            wrapper.appendChild(icon);
            wrapper.appendChild(label);
            image.dataset.luxFallbackApplied = "true";
            image.style.display = "none";
            image.parentNode.insertBefore(wrapper, image.nextSibling);
        }

        forEach("img", function (image) {
            image.addEventListener("error", function () {
                replaceBrokenImage(image);
            });

            if (image.complete && image.naturalWidth === 0) {
                replaceBrokenImage(image);
            }
        });
    }

    ready(function () {
        document.documentElement.classList.add("luxury-ready");
        applyImageFallbacks();
        createAmbientCanvas();
        applyRevealSystem();
        addMagneticHover();
    });
}());
