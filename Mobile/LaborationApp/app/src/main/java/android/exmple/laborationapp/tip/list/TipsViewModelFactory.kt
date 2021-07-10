package android.exmple.laborationapp.tip.list

import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.models.Repository
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class TipsViewModelFactory(private val repository: Repository, private val userId: String)
    : ViewModelProvider.Factory{

    override fun <T: ViewModel?> create(modelClass: Class<T>) :T {
        return TipsViewModel(repository, userId) as T
    }
}