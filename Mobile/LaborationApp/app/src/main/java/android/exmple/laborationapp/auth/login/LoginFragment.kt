package android.exmple.laborationapp.auth.login

import AppPreferences
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.exmple.laborationapp.R
import android.exmple.laborationapp.databinding.LoginFragmentBinding
import android.exmple.laborationapp.models.Repository
import android.widget.Button
import androidx.databinding.DataBindingUtil
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.Navigation


class LoginFragment : Fragment() {

    private lateinit var viewModel: LoginViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        var binding: LoginFragmentBinding = DataBindingUtil.inflate(
            inflater, R.layout.login_fragment, container, false
        )


        val repository = Repository()
        val viewModelFactory = LoginViewModelFactory(repository)

        viewModel = ViewModelProvider(this, viewModelFactory)
            .get(LoginViewModel::class.java)

        binding.toRegister.setOnClickListener { view:View ->
            Navigation.findNavController(view).navigate(R.id.action_loginFragment_to_registerFragment)
        }

        binding.loginBtn.setOnClickListener { view:View ->

            var password = binding.password.text.toString()
            var email = binding.email.text.toString()
            viewModel.login(password, email)

            viewModel.loggedInUser.observe(viewLifecycleOwner, Observer { response ->
                if(response.isSuccessful){
                    AppPreferences.UserId = response.body()?.id.toString()
                    AppPreferences.Token = response.body()?.token.toString()
                    Navigation.findNavController(view).navigate(R.id.action_loginFragment_to_tipCreateFragment)
                }else{
                }
            })
        }

        return binding.root
    }
}