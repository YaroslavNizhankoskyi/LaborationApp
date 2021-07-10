package android.exmple.laborationapp.tip.list

import android.exmple.laborationapp.api.RetrofitInstance
import android.exmple.laborationapp.models.Repository
import android.exmple.laborationapp.models.UserTip
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch
import java.util.*

class TipsViewModel(private val repository: Repository, private val userId: String) : ViewModel() {

    private val _properties = MutableLiveData<List<UserTip>>()

    val properties: LiveData<List<UserTip>>
        get() = _properties

    fun getDummyTips(){
        val tips = listOf(
            UserTip(12, "Test1", "wadada", "adwdawd",  false),
            UserTip(12, "Test2", "wadada", "adwdawd",  false),
            UserTip(12, "Test3", "wadada", "adwdawd",  false),
            UserTip(12, "Test4", "wadada", "adwdawd",  false)
        )
        _properties.value = tips;
    }

    fun getUserTips() {

        viewModelScope.launch {
            var listResult = repository.GetUserTips(userId)
            if (listResult.size > 0) {
                _properties.value = listResult
            }
        }
    }

}