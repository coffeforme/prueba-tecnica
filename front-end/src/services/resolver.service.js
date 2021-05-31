const resolver = [
    resp => {
        return Promise.resolve(resp.data)
    },
    error => {
        return Promise.reject(error)
    }
]

export { resolver }