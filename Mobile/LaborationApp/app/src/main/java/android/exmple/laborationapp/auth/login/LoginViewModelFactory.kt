package android.exmple.laborationapp.auth.login

import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class LoginViewModelFactory(private val repository: Repository)
: ViewModelProvider.Factory{

    override fun <T: ViewModel?> create(modelClass: Class<T>) :T {
        return LoginViewModel(repository) as T
    }
}