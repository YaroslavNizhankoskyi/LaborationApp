package android.exmple.laborationapp.models

import android.exmple.laborationapp.api.RetrofitInstance
import retrofit2.Response

class Repository {

    suspend fun Login(creds: LoginUser): Response<LoginResponse> {
        return RetrofitInstance.RetrofitInstance.api.login(creds);
    }

    suspend fun Register(creds: RegisterModel): Response<LoginResponse> {
        return RetrofitInstance.RetrofitInstance.api.register(creds);
    }

    suspend fun GetUserTips(userId: String): List<UserTip> {
        return RetrofitInstance.RetrofitInstance.api.getUserTips(userId);
    }

    suspend fun Register(): Response<List<Factor>> {
        return RetrofitInstance.RetrofitInstance.api.getFactors();
    }

    suspend fun CreateUserTip(characteristics: UserCharacteristics): Response<Void>{
        return RetrofitInstance.RetrofitInstance.api.createUserTip(characteristics)
    }

}