package android.exmple.laborationapp.auth.login

import android.exmple.laborationapp.models.LoginResponse
import android.exmple.laborationapp.models.LoginUser
import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch
import retrofit2.Response

class LoginViewModel(private val repository: Repository) : ViewModel() {

    val loggedInUser: MutableLiveData<Response<LoginResponse>> = MutableLiveData()

    fun login(password: String, email: String){
        viewModelScope.launch {
            val creds = LoginUser(password, email)
            val response = repository.Login(creds)
            loggedInUser.value = response
        }
    }
}