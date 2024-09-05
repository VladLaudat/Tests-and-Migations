import axios from 'axios'

function handleRegisterSubmit(event)
{
    event.preventDefault();
    const data =new FormData(event.currentTarget);
    console.log(data);
    axios.post('http://localhost:5203/auth/signup', data)
    .then(function (response) {
        console.log("Success");
        window.location.replace('http://localhost:5203/auth/login');
    })
    .catch(function (error) {
        console.error('Error creating post:', error);
    })
}

export default handleRegisterSubmit;