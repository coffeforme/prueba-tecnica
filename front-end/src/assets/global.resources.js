const resources = {
    es: {
        action: {
            create: "Crear",
            delete: "Delete",
            update: "Actualizar",
            find: "Buscar"
        },
        currentDate: new Date().toISOString().substr(0, 10),
        user: {
            new: "Crear usuario"
        },
        common: {
            id: "Identificación",
            name: "Nombre",
            lastname: "Apellido",
            cel: "Celular",
            tel: "Telefono",
            username: "Usuario",
            email: "E-mail",
            birth: "Fecha de nacimiento",
            actions: "Acciones",
            password: "Contraseña",
            repeatPassword: "Confirmar contraseña",
            address: "Dirección",
            user: "Usuario",
            rol: "Rol",
            required: "Campo requerido"
        }
    }
}

export { resources };