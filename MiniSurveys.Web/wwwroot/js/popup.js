const lockPadding = document.querySelectorAll(".lock-padding");
const body = document.querySelector('body');

let unlock = true;
const timeout = 800;

function popupOpen(curentPopup) {
    if (curentPopup && unlock) {
        const popupActive = document.querySelector('.popup.open-popup');
        if (popupActive) {
            popupClose(popupActive, false);
        } else {
            bodyLock();
        }
        curentPopup.addClass('open-popup');
        curentPopup.bind("click", function (e) {
            if (!e.target.closest('.popup-content')) {
                popupClose(e.target.closest('.popup'));
            }
        })
    }
}

function popupClose(doUnlock = true) {
    if (unlock) {
        const popupActive = document.querySelector('.popup.open-popup');
        popupActive.classList.remove('open-popup');
        if (doUnlock) {
            bodyUnLock();
        }
    }
}

function bodyLock() {
    const lockPaddingValue = window.innerWidth - document.querySelector('.wrapper').offsetWidth + 'px';
    if (lockPadding.length > 0) {
        for (let index = 0; index < lockPadding.length; index++) {
            const el = lockPadding[index];
            el.style.paddingRight = lockPaddingValue;
        }
    }
    body.style.paddingRight = lockPaddingValue;
    body.classList.add('lock');

    unlock = false;
    setTimeout(function () {
        unlock = true;
    }, timeout);
}

function bodyUnLock() {
    setTimeout(function () {
        if (lockPadding.length > 0) {
            for (let index = 0; index < lockPadding.length; index++) {
                const el = lockPadding[index];
                el.style.paddingRight = '0px';
            }
        }
        body.style.paddingRight = '0px';
        body.classList.remove('lock');
    }, timeout);

    unlock = false;
    setTimeout(function () {
        unlock = true;
    }, timeout);
}

document.addEventListener('keydown', function (e) {
    if (e.which === 27) {
        const popupActive = document.querySelector('.popup.open-popup');
        popupClose(popupActive);
    }
});