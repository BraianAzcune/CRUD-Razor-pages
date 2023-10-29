namespace TutorialEU.common.interfaces;

public class ResultadoOperacion {
    public bool Error { get; set; }
    public string MensajeUsuario { get; set; }
    private string _mMensajeDesarrollador = "";
    public string MensajeDesarrollador {
        get { return _mMensajeDesarrollador; }
        set {
#if DEBUG
            MensajeUsuario = "DEBUG MODE= " + MensajeUsuario + MensajeDesarrollador;
#endif
        }
    }

    public ResultadoOperacion(bool error, string mensajeUsuario, string mensajeDesarrollador) {
        Error = error;
        if (mensajeUsuario.Length == 0) {
            mensajeUsuario = "Error desconocido contacte con el administrador";
        } else {
            MensajeUsuario = mensajeUsuario;
        }
        MensajeDesarrollador = mensajeDesarrollador;
#if DEBUG
        MensajeUsuario = "DEBUG MODE= " + MensajeUsuario + MensajeDesarrollador;
#endif
    }
}
