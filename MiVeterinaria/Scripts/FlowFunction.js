function Login() {

    var username = $('#username').val();
    var password = $('#password').val();

    if (username && password) {
        let parametro = {
            NombreUsuario: username,
            Contraseña: password,
        };

        $.ajax({
            url: "/Account/Login",
            type: 'POST',
            data: JSON.stringify(parametro),
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    document.cookie = "AuthToken=" + response.token + "; path=/; secure; httponly;";
                    window.location.href = 'Home/Index';
                } else {
                    Swal.fire(
                        'ERROR',
                        'Usuario o contraseña incorrectos.',
                        'success');
                }
            },
            error: function () {
                Swal.fire(
                    'ERROR',
                    'Error al procesar la solicitud.',
                    'success');
            }
        });
    } else {
        Swal.fire(
            'ERROR',
            'Ingresa un usuario y contraseña',
            'success');
    }

};

function crearUsuario(model) {
    console.log(model);

    var response = validarDataUsuario();

    if (response.valid) {
        var nombre = $("#Nombre").val();
        var apellido = $("#Apellido").val();
        var tipoDoc = $("#TipoDoc").val();
        var numeroDoc = $("#NumeroDoc").val();
        var nombreUsuario = $("#NombreUsuario").val();
        var contraseña = $("#Contraseña").val();
        var confirmarContraseña = $("#ConfirmarContraseña").val();
        var telefono = $("#Telefono").val();

        // Crear un objeto con los datos
        var usuarioData = {
            Nombre: nombre,
            Apellido: apellido,
            TipoDoc: tipoDoc,
            NumeroDoc: numeroDoc,
            Usuario: nombreUsuario,
            Contraseña: contraseña,
            ConfirmarContraseña: confirmarContraseña,
            Telefono: telefono
        };

        $.ajax({
            url: "/Usuarios/InsertarUsuario",
            type: 'POST',
            data: JSON.stringify(usuarioData),
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                // Manejar la respuesta del servidor
                Swal.fire(
                    'Creado',
                    data.message,
                    'success');
                window.location.href = '../';
            },
            error: function (xhr, status, error) {
                // Manejar errores
                Swal.fire(
                    'Opps!',
                    response.Mensaje,
                    'error');
            }
        });
    }
    else {
        mostrarModal("ERROR", response.Mensaje);
    }
}

function validarDataUsuario() {
    var response = {
        valid: true,
        Mensaje: "",
    };

    // Validar Nombre
    if ($("#Nombre").val().trim() === "") {
        response.valid = false
        response.Mensaje = "El nombre es obligatorio."
        return response;
    }

    // Validar Apellido
    if ($("#Apellido").val().trim() === "") {
        response.valid = false
        response.Mensaje = "El apellido es obligatorio."
        return response;
    }

    // Validar TipoDoc
    if ($("#TipoDoc").val() === "" || $("#TipoDoc").val() === null) {
        response.valid = false
        response.Mensaje = "El tipo de documento es obligatorio."
        return response;
    }

    // Validar NumeroDoc
    if ($("#NumeroDoc").val().trim() === "") {
        response.valid = false
        response.Mensaje = "El número de documento es obligatorio."
        return response;
    }

    // Validar NombreUsuario
    if ($("#NombreUsuario").val().trim() === "") {
        response.valid = false
        response.Mensaje = "El nombre de usuario es obligatorio."
        return response;
    }

    // Validar Contraseña
    var contraseña = $("#Contraseña").val().trim();
    if (contraseña === "") {
        response.valid = false
        response.Mensaje = "La contraseña es obligatoria."
        return response;
    }

    // Validar ConfirmarContraseña
    var confirmarContraseña = $("#ConfirmarContraseña").val().trim();
    if (confirmarContraseña === "") {
        response.valid = false
        response.Mensaje = "Confirmar contraseña es obligatorio."
        return response;
    } else if (contraseña !== confirmarContraseña) {
        response.valid = false
        response.Mensaje = "Las contraseñas no coinciden."
        return response;
    }

    // Validar Telefono
    if ($("#Telefono").val().trim() === "") {
        response.valid = false
        response.Mensaje = "El teléfono es obligatorio."
        return response;
    }

    return response;
};

function mostrarModal(title, message) {
    $('#customModalLabel').text(title);
    $('#customModalMessage').text(message);
    $('#customModal').modal('show');
}

function ocultarModal() {
    $('#customModalAcceptButton').click(function () {
        $('#customModal').modal('hide');
    });
};