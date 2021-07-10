package android.exmple.laborationapp.tip.create

import android.exmple.laborationapp.api.RetrofitInstance
import android.exmple.laborationapp.models.*
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch
import retrofit2.Response

class TipCreateViewModel(private val repository: Repository) : ViewModel() {

    val factors: MutableLiveData<Response<List<Factor>>> = MutableLiveData()
    val tipcreateResponse: MutableLiveData<Response<Void>> = MutableLiveData()

    fun getFactors(){
        viewModelScope.launch {
            val response = RetrofitInstance.RetrofitInstance.api.getFactors()
            factors.value = response
        }
    }

    fun createTip(characteristics: UserCharacteristics){
        viewModelScope.launch {
            val response = repository.CreateUserTip(characteristics)
            tipcreateResponse.value = response
        }
    }
}