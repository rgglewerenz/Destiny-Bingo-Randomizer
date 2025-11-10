function fitTextToBox(box, text) {
    let fontSize = 100; // start big
    text.style.fontSize = fontSize + "px";

    const boxRect = box.getBoundingClientRect();

    while (
        (text.scrollWidth > boxRect.width || text.scrollHeight > boxRect.height) &&
        fontSize > 1
    ) {
        fontSize-= 5;
        text.style.fontSize = fontSize + "px";
    }
}

// Example: automatically resize on window change

function resize() {

    const boxes = document.getElementsByClassName("fit-box");
    for (let i = 0; i < boxes.length; i++) {
        fitTextToBox(boxes[i], boxes[i].firstElementChild);
    }
}
window.addEventListener("resize", resize);
resize();
