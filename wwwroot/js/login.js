
let loginUri = '/User/Login'
let token;
const varify = () => {
  const username = document.getElementById('uname');
  const password = document.getElementById('pwd');
  const user = {
    Id: "",
    UserName: username.value.trim(),
    Classification: "",
    Password: password.value.trim()
  };

  fetch(loginUri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(user)

  })
    .then(response => {
      if (response.ok) {
        return response.text();
      }
      else
        throw new Error('Invalid');
    })
    .then(res => {
      const classification = parseJwt(res.split('\"')[1]);
      sessionStorage.setItem('auth', "Bearer ".concat(res.split('\"')[1]));
      if (classification.Classification === "admin") {
        window.location.href = './UserManagement.html';
      }
      else {
        window.location.href = './TaskManagement.html';
      }
    })
    .catch(error => window.location.href = './error.html');
}


function parseJwt(token) {
  var base64Url = token.split('.')[1];
  var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
  }).join(''));

  return JSON.parse(jsonPayload);
}





//////////////////////////////////
const body = document.querySelector("body");
const modal = document.querySelector(".modal");
const modalButton = document.querySelector(".modal-button");
const closeButton = document.querySelector(".close-button");
const scrollDown = document.querySelector(".scroll-down");
let isOpened = false;

const openModal = () => {
  modal.classList.add("is-open");
  body.style.overflow = "hidden";
};

const closeModal = () => {
  modal.classList.remove("is-open");
  body.style.overflow = "initial";
};

window.addEventListener("scroll", () => {
  if (window.scrollY > window.innerHeight / 3 && !isOpened) {
    isOpened = true;
    scrollDown.style.display = "none";
    openModal();
  }
});

modalButton.addEventListener("click", openModal);
closeButton.addEventListener("click", closeModal);

document.onkeydown = evt => {
  evt = evt || window.event;
  evt.keyCode === 27 ? closeModal() : false;
};