export default function authHeader() {
  let token = JSON.parse(localStorage.getItem('token'));

  if (token && token.data) {
    return {
      Authorization: 'Bearer ' + token.data
    };
  } else {
    return {};
  }
}
