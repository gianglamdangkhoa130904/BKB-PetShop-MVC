document.getElementById('nextrabbit').onclick = function () {
    const widthItem = document.querySelector('.itemrabbit').offsetWidth;
    document.getElementById('formlistrabbit').scrollLeft += 320
}
document.getElementById('prevrabbit').onclick = function () {
    const widthItem = document.querySelector('.itemrabbit').offsetWidth;
    document.getElementById('formlistrabbit').scrollLeft -= 320
}