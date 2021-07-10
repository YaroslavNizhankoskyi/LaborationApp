package android.exmple.laborationapp.api

import android.exmple.laborationapp.models.*
import retrofit2.Response
import retrofit2.http.*

interface Api {
    @Headers("Content-Type:application/json")
    @POST("account/login")
    suspend fun login(@Body user: LoginUser): Response<LoginResponse>

    @POST("account/register")
    suspend fun register(@Body user: RegisterModel): Response<LoginResponse>

    @GET("tip/user/{userId}")
    suspend fun getUserTips(@Path("userId") userId: String): List<UserTip>

    @Headers("Content-Type:application/json")
    @GET("admin/factors")
    suspend fun getFactors(): Response<List<Factor>>

    @Headers("Content-Type:application/json")
    @POST("tip/user")
    suspend fun createUserTip(@Body userCharacteristics: UserCharacteristics): Response<Void>
}