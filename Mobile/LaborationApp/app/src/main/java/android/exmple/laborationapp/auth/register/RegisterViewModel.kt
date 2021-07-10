package android.exmple.laborationapp.auth.register

import android.exmple.laborationapp.models.LoginResponse
import android.exmple.laborationapp.models.LoginUser
import android.exmple.laborationapp.models.RegisterModel
import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch
import retrofit2.Response

class RegisterViewModel(private val repository: Repository) : ViewModel() {


    val loggedInUser: MutableLiveData<Response<LoginResponse>> = MutableLiveData()

    fun register(model: RegisterModel){
        viewModelScope.launch {
            val response = repository.Register(model)
            loggedInUser.value = response
        }
    }
}