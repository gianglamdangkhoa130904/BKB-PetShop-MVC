document.getElementById('nextsale').onclick = function () {
    const widthItem = document.querySelector('.itemsale').offsetWidth;
    document.getElementById('formlistsale').scrollLeft += 250
}
document.getElementById('prevsale').onclick = function () {
    const widthItem = document.querySelector('.itemsale').offsetWidth;
    document.getElementById('formlistsale').scrollLeft -= 250
}