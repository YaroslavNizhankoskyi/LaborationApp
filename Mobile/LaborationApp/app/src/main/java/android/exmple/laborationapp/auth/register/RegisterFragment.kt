package android.exmple.laborationapp.auth.register

import android.exmple.laborationapp.R
import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.auth.login.LoginViewModelFactory
import android.exmple.laborationapp.databinding.RegisterFragmentBinding
import android.exmple.laborationapp.models.RegisterModel
import android.exmple.laborationapp.models.Repository
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContentProviderCompat.requireContext
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.Navigation

class RegisterFragment: Fragment() {

    private lateinit var viewModel: RegisterViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        var binding: RegisterFragmentBinding = DataBindingUtil.inflate(
            inflater, R.layout.register_fragment, container, false
        )



        val repository = Repository()
        val viewModelFactory = RegisterViewModelFactory(repository)
        viewModel = ViewModelProvider(this, viewModelFactory)
            .get(RegisterViewModel::class.java)

        binding.toLogin.setOnClickListener { view:View ->
            Navigation.findNavController(view).navigate(R.id.action_registerFragment_to_loginFragment)
        }

        binding.loginBtn.setOnClickListener { view: View ->

            var password = binding.password.text.toString()
            var email = binding.email.text.toString()
            var register = RegisterModel(password, email, "", "", 0, "");
            viewModel.register(register);

            viewModel.loggedInUser.observe(viewLifecycleOwner, Observer { response ->
                if(response.isSuccessful){
                    //Navigation.findNavController(view).navigate(R.id.action_loginFragment_to_navigation_drug)
                }else{
                }
            })
        }

        return binding.root
    }
}