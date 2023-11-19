document.getElementById('nexthamster').onclick = function () {
    const widthItem = document.querySelector('.itemhamster').offsetWidth;
    document.getElementById('formlisthamster').scrollLeft += 370
}
document.getElementById('prevhamster').onclick = function () {
    const widthItem = document.querySelector('.itemhamster').offsetWidth;
    document.getElementById('formlisthamster').scrollLeft -= 370
}