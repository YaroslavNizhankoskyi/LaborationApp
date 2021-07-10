package android.exmple.laborationapp.tip.create

import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class TipCreateViewFactory(private val repository: Repository)
    : ViewModelProvider.Factory{

    override fun <T: ViewModel?> create(modelClass: Class<T>) :T {
        return TipCreateViewModel(repository) as T
    }
}