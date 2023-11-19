document.getElementById('nextdog').onclick = function () {
    const widthItem = document.querySelector('.itemdog').offsetWidth;
    document.getElementById('formlistdog').scrollLeft += 320
}
document.getElementById('prevdog').onclick = function () {
    const widthItem = document.querySelector('.itemdog').offsetWidth;
    document.getElementById('formlistdog').scrollLeft -= 320
}