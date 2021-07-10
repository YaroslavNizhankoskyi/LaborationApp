package android.exmple.laborationapp.models

data class LoginResponse (
    val firstName: String,
    val token: String,
    val email: String,
    val photoUrl: String,
    val id: String
    )