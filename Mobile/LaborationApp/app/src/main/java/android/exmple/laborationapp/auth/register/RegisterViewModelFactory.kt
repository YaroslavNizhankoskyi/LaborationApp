package android.exmple.laborationapp.auth.register

import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class RegisterViewModelFactory(private val repository: Repository)
    : ViewModelProvider.Factory{

    override fun <T: ViewModel?> create(modelClass: Class<T>) :T {
        return RegisterViewModel(repository) as T
    }
}