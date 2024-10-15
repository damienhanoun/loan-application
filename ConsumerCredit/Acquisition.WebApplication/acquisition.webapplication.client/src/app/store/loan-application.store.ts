﻿import {
  patchState,
  signalStore,
  watchState,
  withHooks,
  withMethods,
  withState,
} from '@ngrx/signals';
import {
  withDevtools,
  withStorageSync,
} from '@angular-architects/ngrx-toolkit';
import { inject, Injectable } from '@angular/core';

type LoanApplicationStore = {
  loanApplicationId: string | null;
  userInformation: {
    initialLoanWish: {
      project: string | null;
      amount: string | null;
      maturity: string | null;
    };
    email: string | null;
  };
};

const initialState: LoanApplicationStore = {
  loanApplicationId: null,
  userInformation: {
    initialLoanWish: {
      project: null,
      amount: null,
      maturity: null,
    },
    email: null,
  },
};

export const LoanApplicationStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods((store) => ({
    setLoanApplicationId(loanApplicationId: string | null): void {
      patchState(store, (_) => ({
        loanApplicationId: loanApplicationId,
      }));
    },
    updateEmail(email: string | null): void {
      patchState(store, (state) => ({
        userInformation: {
          ...state.userInformation,
          email: email,
        },
      }));
    },
    updateLoanWishField(
      field: keyof (typeof initialState)['userInformation']['initialLoanWish'],
      value: string | null,
    ): void {
      patchState(store, (state) => ({
        userInformation: {
          ...state.userInformation,
          initialLoanWish: {
            ...state.userInformation.initialLoanWish,
            [field]: value,
          },
        },
      }));
    },
  })),
  withHooks({
    onInit(store) {
      watchState(store, (state) => {
        console.log('[watchState]', state.userInformation);
      });
    },
  }),
  withDevtools('loanApplicationStore'),
  // Handle page reload when using F5
  withStorageSync({
    key: 'LoanApplicationStore',
    storage: () => sessionStorage,
  }),
);

// Handle the fact of keeping one version of the state
@Injectable({
  providedIn: 'root',
})
export class LoanApplicationStoreService {
  public store = inject(LoanApplicationStore);
}
