const API = {
    ROOT: process.env.VUE_APP_API_ROOT,
    AUTH_PATH: 'api/v1/auth/',
    PAGEROLE_PATH: 'api/v1/parameter/pagerole/',
    ROLE_PATH: 'api/v1/auth/role/',
}

const ENVIROMENT = {
    dev: false,
    dark: false,
    APP_NAME: process.env.VUE_APP_TITLE,
    image_logo: process.env.VUE_APP_LOGO
}
export { API, ENVIROMENT }