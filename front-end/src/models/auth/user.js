export default class User {
    constructor(uid, username, password, email, firstname, lastname, fullname, cel, address, salt, requirechangepass, confirmationcode, sayhi, emailconfirmed, state, date, pass, id_rol, role) {
        this.uid = uid;
        this.username = username;
        this.password = password;
        this.email = email;
        this.firstname = firstname;
        this.lastname = lastname;
        this.fullname = fullname;
        this.cel = cel;
        this.address = address;
        this.salt = salt;
        this.requirechangepass = requirechangepass;
        this.confirmationcode = confirmationcode;
        this.sayhi = sayhi;
        this.emailconfirmed = emailconfirmed;
        this.state = state;
        this.date = date;
        this.pass = pass;
        this.id_rol = id_rol;
        this.role = role;
    }
}
