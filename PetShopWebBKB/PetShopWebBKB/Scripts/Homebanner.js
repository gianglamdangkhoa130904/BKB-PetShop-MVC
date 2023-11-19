const listImage = document.querySelector('.list_images')
const imgs = document.getElementsByTagName('img')
const btnLeft = document.querySelector('#left')
const btnRight = document.querySelector('#right')
const length = imgs.length
let current = 0
const handleChangeSlide = () => {
    if (current == length - 5) {
        current = 0
        let width = imgs[0].offsetWidth
        listImage.style.transform = `translateX(0px)`
    }
    else {
        current++
        listImage.style.transform = `translateX(${800 * -1 * current}px)`
    }
}
let handleEventChangslide = setInterval(handleChangeSlide, 4000)

btnRight.addEventListener('click', () => {
    clearInterval(handleEventChangslide)
    handleChangeSlide()
    handleEventChangslide = setInterval(handleChangeSlide, 4000)
})
btnLeft.addEventListener('click', () => {
    clearInterval(handleEventChangslide)
    if (current == 0) {
        current = length - 5
        listImage.style.transform = `translateX(${800 * -1 * current}px)`
    }
    else {
        current--
        listImage.style.transform = `translateX(${800 * -1 * current}px)`
    }
    handleEventChangslide = setInterval(handleChangeSlide, 4000)
})