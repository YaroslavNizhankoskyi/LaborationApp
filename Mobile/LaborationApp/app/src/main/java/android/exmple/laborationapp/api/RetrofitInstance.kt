package android.exmple.laborationapp.api

import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

object RetrofitInstance {

    object RetrofitInstance {
        private val BASE_URL = "https://1ad944e75633.ngrok.io/api/"


        private val retrofit by lazy{
            Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .client(client)
                .build()
        }

        private var client = OkHttpClient.Builder()
            .addInterceptor(ServiceInterceptor())
            .readTimeout(60, TimeUnit.SECONDS)
            .writeTimeout(60, TimeUnit.SECONDS)   // socket timeout
            .build()

        val api: Api by lazy {
            retrofit.create(Api::class.java)
        }

    }
}