const marquee = document.getElementById("bannerText");
const messages = marquee.querySelectorAll("p");
let index = 0;

marquee.addEventListener("animationiteration", () => {
    messages[index].classList.remove("active");
    index = (index + 1) % messages.length;
    messages[index].classList.add("active");
});

marquee.addEventListener("mouseenter", () => {
    marquee.style.animationPlayState = "paused";
});

marquee.addEventListener("mouseleave", () => {
    marquee.style.animationPlayState = "running";
});