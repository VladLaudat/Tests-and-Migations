import axios from 'axios'
import Cookies from 'js-cookie'

function handleLoginSubmit(event)
{
    event.preventDefault();
    const data =new FormData(event.currentTarget);
    console.log(data);
    axios.post('http://localhost:5203/auth/login', data)
    .then(function (response) {
        console.log("Success");
        Cookies.set('authToken', response.data.token, {expires: 1});
        setTimeout(() => {window.location.reload(false)},500);
    })
    .catch(function (error) {
        console.error('Error creating post:', error);
    })
}

export default handleLoginSubmit;