package android.exmple.laborationapp.api

import AppPreferences
import okhttp3.Interceptor
import okhttp3.Response

class ServiceInterceptor : Interceptor{
    var token : String = "";

    fun Token(token: String ) {
        this.token = token;
    }

    override fun intercept(chain: Interceptor.Chain): Response {
        var request = chain.request()

        Token(AppPreferences.Token!!)
        if(request.header("No-Authentication")==null){

            if(!token.isNullOrEmpty())
            {
                val finalToken =  "Bearer "+token
                request = request.newBuilder()
                    .addHeader("Authorization",finalToken)
                    .build()
            }

        }

        return chain.proceed(request)
    }
}