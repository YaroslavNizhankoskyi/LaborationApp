package android.exmple.laborationapp.tip.list

import android.exmple.laborationapp.R
import android.exmple.laborationapp.auth.login.LoginViewModel
import android.exmple.laborationapp.auth.login.LoginViewModelFactory
import android.exmple.laborationapp.databinding.LoginFragmentBinding
import android.exmple.laborationapp.models.Repository
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import kotlinx.android.synthetic.main.tip_fragment.*

class TipsFragment : Fragment() {

    private lateinit var viewModel: TipsViewModel

    companion object {
        fun newInstance() = TipsFragment();
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ) : View?{
        return inflater.inflate(R.layout.tip_fragment, container, false)
    }


    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        var id = AppPreferences.UserId
        val repository = Repository()

        val viewModelFactory = TipsViewModelFactory(repository, id!!)

        viewModel = ViewModelProvider(this, viewModelFactory)
            .get(TipsViewModel::class.java)

        viewModel.getUserTips()

        viewModel.properties.observe(viewLifecycleOwner, Observer { user_tips ->
            tips.also {
                it.layoutManager = LinearLayoutManager(requireContext())
                it.setHasFixedSize(true)
                it.adapter = TipAdapter(user_tips)
            }
        })
    }
}