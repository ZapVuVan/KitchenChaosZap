using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static CuttingCounter;

public class StoveCounter : BaseCounter {
    
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float  fryingTimer;
    FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;

                if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                {
                    fryingTimer = 0f;
                    Debug.Log("Object Fried !");
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.ouput, this);

                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                    burningTimer = 0f;
                    }
                    
                    break;
            case State.Fried:
                burningTimer += Time.deltaTime;

                if (burningTimer > burningRecipeSO.burningTimerMax)
                {
                    Debug.Log("Object Burned");
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.ouput, this);
                    state = State.Burned;
                }
                break;
            case State.Burned:
                break;
        }
        
            
        
           Debug.Log("Frying timer: " + fryingTimer);
        }   
    }
    //private void Start()
    //{
    //    StartCoroutine(HandleFryTimer());
    //}
    //private IEnumerator HandleFryTimer()
    //{
    //    yield return new WaitForSeconds(1f);
    //}

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Plater carrying something that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state =State.Frying;
                    fryingTimer = 0f;
                }
            }
            else
            {
                // PLayer is not carrying anything
            }
        }
        else
        {
            // There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything,
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetInputForOutput(KitchenObjectSO input)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(input);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.ouput;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == input)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == input)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }


}
