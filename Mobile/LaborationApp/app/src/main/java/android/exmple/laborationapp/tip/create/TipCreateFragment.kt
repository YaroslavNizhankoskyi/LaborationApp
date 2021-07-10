package android.exmple.laborationapp.tip.create

import android.exmple.laborationapp.R
import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.auth.login.LoginViewModelFactory
import android.exmple.laborationapp.databinding.TipCreateBinding
import android.exmple.laborationapp.models.Factor
import android.exmple.laborationapp.models.Repository
import android.exmple.laborationapp.models.UserCharacteristics
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.Navigation


class TipCreateFragment: Fragment() {

    private lateinit var viewModel: TipCreateViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        var binding: TipCreateBinding = DataBindingUtil.inflate(
            inflater, R.layout.tip_create, container, false
        )

        var health: List<Factor> = listOf()
        var mental: List<Factor> = listOf()
        var sleep: List<Factor> = listOf()
        var labor: List<Factor> = listOf()

        var repository = Repository()

        val viewModelFactory = TipCreateViewFactory(repository)

        viewModel = ViewModelProvider(this, viewModelFactory)
            .get(TipCreateViewModel::class.java)

        viewModel.getFactors()

        viewModel.factors.observe(viewLifecycleOwner, Observer { response ->
            if(response.isSuccessful){
                var factors = response.body();

                factors!!.forEach { it.factorTypeId.toInt() }

                health = factors!!.filter{ it.factorTypeId == 0 }
                mental = factors!!.filter{ it.factorTypeId == 1 }
                sleep = factors!!.filter{ it.factorTypeId == 2 }
                labor = factors!!.filter{ it.factorTypeId == 3 }

                val adapterHealth: ArrayAdapter<*> = ArrayAdapter<Any?>(
                    requireContext(),
                    android.R.layout.simple_spinner_dropdown_item, health.map { it.name}
                )
                val adapterMental: ArrayAdapter<*> = ArrayAdapter<Any?>(
                    requireContext(),
                    android.R.layout.simple_spinner_dropdown_item, mental.map { it.name }
                )
                val adapterSleep: ArrayAdapter<*> = ArrayAdapter<Any?>(
                    requireContext(),
                    android.R.layout.simple_spinner_dropdown_item, sleep.map {it.name}
                )
                val adapterLabor: ArrayAdapter<*> = ArrayAdapter<Any?>(
                    requireContext(),
                    android.R.layout.simple_spinner_dropdown_item, labor.map {it.name}
                )


                binding.health.setAdapter(adapterHealth)
                binding.mental.setAdapter(adapterMental)
                binding.sleep.setAdapter(adapterSleep)
                binding.labor.setAdapter(adapterLabor)
            }
        })





        binding.send.setOnClickListener { view: View ->

            var healthFactor: String = binding.health.selectedItem as String
            var mentalFactor: String = binding.mental.selectedItem as String
            var sleepFactor: String = binding.sleep.selectedItem as String
            var laborFactor: String = binding.labor.selectedItem as String

            var userCharacteristics = UserCharacteristics(healthFactorId =  health.find { it.name == healthFactor }!!.id,
                mentalFactorId = mental.find { it.name == mentalFactor }!!.id,
                sleepFactorId = sleep.find { it.name == sleepFactor }!!.id,
                laborFactorId =  labor.find { it.name == laborFactor }!!.id)

            viewModel.createTip(userCharacteristics)

            viewModel.tipcreateResponse.observe(viewLifecycleOwner, Observer { response ->
                if(response.isSuccessful) {

                }
                Navigation.findNavController(view).navigate(R.id.action_tipCreateFragment_to_tipsFragment)
            })


        }

        binding.viewTips.setOnClickListener { view: View ->
            Navigation.findNavController(view).navigate(R.id.action_tipCreateFragment_to_tipsFragment)
        }



        return binding.root
    }
}