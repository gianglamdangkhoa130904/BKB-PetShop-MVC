document.getElementById('nextcat').onclick = function () {
    const widthItem = document.querySelector('.itemcat').offsetWidth;
    document.getElementById('formlistcat').scrollLeft += 250
}
document.getElementById('prevcat').onclick = function () {
    const widthItem = document.querySelector('.itemcat').offsetWidth;
    document.getElementById('formlistcat').scrollLeft -= 250
}