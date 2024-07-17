export const textConstants = {
  components: {
    layout: {
      footer: {
        information: "© Papirus - Todos los derechos reservados",
        links: {
          privacyPolicy: "Política de privacidad",
        },
      },
      navBar: {
        links: {
          processes: "Procesos",
          administration: "Administración",
          demands: "Demandas",
          guardianships: "Tutelas",
          config: "Configuración",
          teams: "Equipos",
          security: "Seguridad",
          users: "Usuarios",
        },
        userLinks: {
          updatePassword: "Actualizar contraseña",
        },
      },
    },
    auth: {
      signin: {
        welcome: "Bienvenido",
        subtitle: "Ingresa tus datos",
        email: "Correo",
        password: "Contraseña",
        logIn: "Ingresar",
      },
      logout: {
        button: "Cerrar sesión",
      },
    },
    dialog: {
      signout: {
        title: "Cierre de sesión",
        confirmText: "¿Realmente desea cerrar esta sesión?",
        buttonText: "Sí, cerrar mi sesión",
      },
    },
    messages: {
      error: {
        accessDenied: "No tienes permiso para iniciar sesión.",
        credentialsSignin: "Error al autenticar al usuario.",
        fetch: "Se produjo un error. Intente nuevamente.",
        default: "Contacte al soporte técnico para obtener asistencia.",
        credentialsError: "Información incorrecta. Intente nuevamente.",
        unknown: "Error desconocido",
        validation: "Error de validación de campos",
        search: "Error al buscar la información",
        unauthorized: "No tiene permisos para realizar esta acción",
        signin: "Error al iniciar sesión",
        notFound: "No se encontró la información solicitada",
        fileNotCompatible: "No se puede visualizar el archivo",
        fileNotFound: "No se encontró el archivo",
      },
      success: {
        successAction: "Enhorabuena",
        createUser: "Usuario creado correctamente",
        updateUser: "Usuario actualizado correctamente",
        createTeam: "Equipo creado correctamente",
        updateTeam: "Equipo actualizado correctamente",
        deleteTeam: "Equipo eliminado correctamente",
        createTeamMember: "Miembro de Equipo creado correctamente",
        updateTeamMember: "Miembro de Equipo actualizado correctamente",
        deleteTeamMember: "Miembro de Equipo eliminado correctamente",
      },
      tryAgain: "Intentar nuevamente",
    },
    session: {
      status: {
        authenticated: "Autenticado",
        loading: "Cargando",
        unauthenticated: "No autenticado",
      },
    },
    loading: {
      loadingView: "Cargando",
    },
    alt: {
      logo: "Logo de Papirus",
      icons: {
        search: "Icono de búsqueda",
        clear: "Icono de limpiar",
        user: "Icono del usuario",
        logout: "Icono de cierre de sesión",
        divider: "Icono de división",
        edit: "Icono de edición",
        previous: "Icono anterior",
        next: "Icono siguiente",
        down: "Icono abajo",
        save: "Icono de guardar",
        cancel: "Icono de cancelar",
        reassign: "Icono de reasignar",
        access: "Icono de acceso",
        view: "Icono de ver",
        thumbUp: "Icono de pulgar arriba",
      },
    },
    pagination: {
      of: "de",
      elementsPerPage: "Elementos por página",
    },
    usersForm: {
      header: "Información general",
      form: {
        firstName: "Nombre(s)",
        lastName: "Apellidos",
        email: "Correo Electrónico",
        role: "Seleccionar Rol",
        password: "Contraseña",
        confirmPassword: "Confirmar Contraseña",
        guide: "Estos campos son obligatorios",
        cancel: "Cancelar",
        save: "Guardar",
      },
      error: {
        firstName: "Debe tener al menos 2 caracteres.",
        lastName: "Debe tener al menos 2 caracteres.",
        email: "Formato de correo electrónico inválido.",
        role: "Este campo es requerido, selecciona uno por favor.",
        password:
          "Debe tener al menos 8 caracteres. Un carácter especial, un número, una letra mayúscula y una letra minúscula.",
        confirmPassword: "Las contraseñas no coinciden.",
        save: "Error al guardar la información.",
      },
    },
    common: {
      search: "Buscar",
      download: "Descargar",
    },
  },
  pages: {
    processes: {
      pageTitle: "Papirus | Procesos",
      pageDescription: "Lista de procesos de Papirus",
      title: "Selecciona el tipo de proceso a operar",
      options: {
        demands: "Demandas",
        guardianships: "Tutelas",
      },
    },
    users: {
      pageTitle: "Papirus | Usuarios",
      pageDescription: "Lista de usuarios de Papirus",
      header: {
        title: "Usuarios",
        search: "Buscar",
        button: "Agregar",
      },
      table: {
        headers: {
          firstName: "Nombre",
          lastName: "Apellidos",
          email: "Correo electrónico",
          role: "Rol",
          status: "Estatus",
        },
        rows: {
          status: {
            active: "Activo",
            inactive: "Inactivo",
          },
        },
      },
    },
    createUser: {
      pageTitle: "Papirus | Crear Usuario",
      pageDescription: "Crear nuevo usuario de Papirus",
      header: {
        title: "Crear Usuario",
        button: "Regresar",
      },
      content: {
        header: "Información general",
        form: {
          firstName: "Nombre(s)",
          lastName: "Apellidos",
          email: "Correo Electrónico",
          role: "Seleccionar Rol",
          password: "Contraseña",
          confirmPassword: "Confirmar Contraseña",
          guide: "Estos campos son obligatorios",
          cancel: "Cancelar",
          save: "Guardar",
        },
      },
      error: {
        title: "Error al crear el usuario",
        message: "El usuario no ha sido creado.",
      },
      success: {
        title: "Usuario creado",
        message: "El usuario ha sido creado exitosamente.",
      },
    },
    editUser: {
      pageTitle: "Papirus | Editar Usuario",
      pageDescription: "Editar usuario de Papirus",
      header: {
        title: "Editar Usuario",
        button: "Regresar",
      },
      content: {
        header: "Información general",
        form: {
          firstName: "Nombre(s)",
          lastName: "Apellidos",
          email: "Correo Electrónico",
          role: "Seleccionar Rol",
          password: "Contraseña",
          confirmPassword: "Confirmar Contraseña",
          guide: "Estos campos son obligatorios",
          cancel: "Cancelar",
          save: "Guardar",
        },
      },
      error: {
        title: "Error al editar el usuario",
        message: "El usuario no se ha actualizado.",
      },
      success: {
        title: "Usuario actualizado",
        message: "El usuario ha sido actualizado exitosamente.",
      },
    },
    updatePassword: {
      pageTitle: "Papirus | Actualizar Contraseña",
      pageDescription:
        "Actualizar la contraseña con una nueva contraseña segura",
      header: {
        title: "Actualizar Contraseña",
      },
      form: {
        email: "Correo electrónico",
        currentPassword: "Contraseña actual",
        newPassword: "Nueva contraseña",
        confirmPassword: "Confirmar Contraseña",
        guide:
          "Al actualizar la contraseña, tendrá que volver a iniciar sesión.",
        cancel: "Cancelar",
        save: "Guardar",
      },
      error: {
        title: "Error al actualizar la contraseña",
        message: "La contraseña no se ha actualizado.",
      },
      success: {
        title: "Contraseña actualizada",
        message: "La contraseña ha sido actualizada exitosamente.",
      },
    },
    teams: {
      pageTitle: "Papirus | Equipos",
      pageDescription: "Lista de equipos de Papirus",
      header: {
        title: "Equipos",
        search: "Buscar",
        addTeam: "Agregar",
      },
      table: {
        headers: {
          name: "Nombre",
        },
        rows: {
          status: {
            active: "Activo",
            inactive: "Inactivo",
          },
        },
      },
    },
    guardianships: {
      pageTitle: "Papirus | Tutelas",
      pageDescription: "Lista de tutelas de Papirus",
      title: "Tutelas",
      header: {
        title: "Tutelas",
        search: "Buscar",
      },
      table: {
        headers: {
          submissionDate: "Fecha",
          deadLineDate: "Vencimiento",
          defendantName: "Accionado",
          claimerName: "Accionante",
          submissionIdentifier: "Radicado",
          status: "Estado",
          assignedTo: "Asignada a",
        },
        rows: {
          status: {
            active: "Activo",
            inactive: "Inactivo",
          },
          assignedTo: {
            notAssigned: "No asignada",
          },
        },
      },
      assign: {
        success: {
          title: "Tutela asignada",
          message: "La tutela ha sido asignada exitosamente.",
        },
        error: {
          title: "Error al asignar la tutela",
          message: "La tutela no se ha asignado.",
        },
      },
      error: {
        title: "Error al asignar la tutela",
        message: "La tutela no se ha asignado.",
      },
      success: {
        title: "Tutela asignada",
        message: "La tutela ha sido asignada exitosamente.",
      },
    },
    documents: {
      pageTitle: "Papirus | Documentos",
      pageDescription: "Documentos para descargar o visualizar",
      title: "Documentos",
      header: {
        title: "Documentos",
        button: "Regresar",
      },
      table: {
        headers: {
          documents: "Documentos",
          actions: "Acciones",
        },
      },
      bottomButtons: {
        download: "Descargar",
        continue: "Continuar",
      },
    },
    autoadmit: {
      pageTitle: "Papirus | Información Indentificada AUTOADMITE",
      pageDescription: "Información identificada en el documento de autoadmite",
      title: "AUTOADMITE",
      businessLine: "Línea de Negocio",
      table: {
        title: "Información identificada",
        subtitle: "Datos identificados:",
        headers: {
          field: "Campo",
          value: "Valor",
        },
      },
      bottomButtons: {
        cancel: "Cancelar",
        save: "Guardar",
        continue: "Continuar",
      },
      businessLineLabel: "Linea de negocio",
      onFieldsUpdates: {
        successMessage:
          "Campos actualizados correctamente. Continuando automaticamente...",
        errorMessage: "Ha ocurrido un error actualizando los campos: ",
      },
    },
    email: {
      pageTitle: "Papirus | Información Indentificada Email",
      pageDescription: "Información identificada en el documento de email",
      title: "CORREO ELECTRÓNICO",
      table: {
        title: "Información identificada",
        subtitle: "Datos identificados:",
        headers: {
          field: "Campo",
          value: "Valor",
        },
      },
      bottomButtons: {
        cancel: "Cancelar",
        save: "Guardar",
        continue: "Continuar",
      },
      onFieldsUpdates: {
        successMessage:
          "Campos actualizados correctamente. Continuando automaticamente...",
        errorMessage: "Ha ocurrido un error actualizando los campos: ",
      },
    },
    extractedDataResume: {
      pageTitle: "Papirus | Informacion Identificada",
      pageDescription:
        "Información identificada en el documento de autoadmite y correo electrónico de la Tutela",
      title: "Resumen",
      button: "Regresar",
      table: {
        title: "Datos identificados: ",
        headers: {
          field: "Campo",
          value: "Valor",
        },
      },
      bottomButtons: {
        downloadResponseDocument: "Descargar contestación",
        downloadEmergencyBrief: "Descargar escrito de emergencia",
        generateResponseDocument: "Generar contestación",
        generateEmergencyBrief: "Generar escrito de emergencia",
      },
      confirmation: {
        title: "Confirmación",
        message: "¿Está seguro que desea generar el documento?",
        yes: "Sí",
        no: "No",
      },
    },
    generatedDocument: {
      pageTitle: "Papirus | Documento Final Generado",
      pageDescription:
        "Documento final en para descargar y regresar a la pantalla inicial",
      onDocumentGenerate: {
        success: {
          title: "Documento generado",
          message: "El documento ha sido generado exitosamente.",
        },
        error: {
          title: "Error al generar el documento",
          message: "El documento no se ha generado.",
        },
      },
      success: "Documento generado satisfactoriamente",
      bottomBtn: "Salir",
    },
    notFound: {
      pageTitle: "Papirus | 404",
      pageDescription: "Ruta no encontrada",
      title: "404 | No se pudo encontrar esta página.",
    },
    availableSoon: {
      pageTitle: "Papirus | Disponible Pronto",
      pageDescription: "Ruta en desarrollo",
      title:
        "Esta sección estará disponible pronto. Estamos trabajando para brindarte más y mejores funcionalidades. ¡Gracias por tu paciencia!",
    },
  },
  roles: {
    "1": "Administrador",
    "2": "Usuario",
    "3": "Súper Usuario",
  },
  processStatus: {
    status: {
      "1": "Pendiente Asignación",
      "2": "Asignada",
      "3": "En Progreso",
      "4": "Contestada",
      "5": "Terminada",
    },
    update: {
      success: {
        title: "Estatus actualizado",
        message: "El estatus ha sido actualizado exitosamente.",
      },
      error: {
        title: "Error al actualizar el estatus",
        message: "El estatus no se ha actualizado.",
      },
    },
  },
  filters: {
    apply: "Aplicar",
    clear: "Limpiar",
    matchMode: {
      StartsWith: "Empieza con",
      EndsWith: "Termina con",
      Contains: "Contiene",
      DoesNotContain: "No contiene",
      IsEqualTo: "Es igual a",
      IsNotEqualTo: "No es igual a",
      IsEmpty: "Está vacío",
      IsNotEmpty: "No está vacío",
      IsGreaterThan: "Es mayor que",
      IsGreaterThanOrEqualTo: "Es mayor o igual que",
      IsLessThan: "Es menor que",
      IsLessThanOrEqualTo: "Es menor o igual que",
    },
  },
};
