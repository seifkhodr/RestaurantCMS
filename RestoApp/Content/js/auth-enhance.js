(function () {
    "use strict";

    function ready(callback) {
        if (document.readyState === "loading") {
            document.addEventListener("DOMContentLoaded", callback, { once: true });
        } else {
            callback();
        }
    }

    function enhancePasswords() {
        Array.prototype.forEach.call(document.querySelectorAll("input[type='password']"), function (input) {
            if (input.parentElement && input.parentElement.classList.contains("auth-password-wrap")) {
                return;
            }

            var wrapper = document.createElement("div");
            var toggle = document.createElement("button");

            wrapper.className = "auth-password-wrap";
            input.parentNode.insertBefore(wrapper, input);
            wrapper.appendChild(input);

            toggle.type = "button";
            toggle.className = "auth-password-toggle";
            toggle.setAttribute("aria-label", "Show password");
            toggle.innerHTML = "<i class=\"fa-regular fa-eye\"></i>";

            toggle.addEventListener("click", function () {
                var showing = input.type === "text";
                input.type = showing ? "password" : "text";
                toggle.setAttribute("aria-label", showing ? "Show password" : "Hide password");
                toggle.innerHTML = showing ? "<i class=\"fa-regular fa-eye\"></i>" : "<i class=\"fa-regular fa-eye-slash\"></i>";
            });

            wrapper.appendChild(toggle);
        });
    }

    function polishMessages() {
        Array.prototype.forEach.call(document.querySelectorAll("span[id$='lblMessage']"), function (label) {
            if (label.textContent.trim().length > 0) {
                label.classList.add("auth-message-active");
            }
        });
    }

    ready(function () {
        document.documentElement.classList.add("auth-enhanced");
        enhancePasswords();
        polishMessages();
    });
}());
