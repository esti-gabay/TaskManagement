
let loginUri = '/User/Login'
let token;
const varify = () => {
    const username = document.getElementById('uname');
    const password = document.getElementById('pwd');
    console.log(username);
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
            // username.value = '';
            // password.value=''
            if (response.ok)
                return response.text();
            else
               throw new Error('Invalid');
        })
        .then(res => {
            token = res.replace('"','Bearer ');
            token =token.replace('"','');
            sessionStorage.setItem('auth', token);
        })
        .catch(error => window.location.href='./error.html');
}

