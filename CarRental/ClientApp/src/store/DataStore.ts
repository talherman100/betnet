import { createSlice, configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { AdvancedSearchParams } from '../interfaces';

export interface IIndexable<T = any> { [key: string]: T }

export interface StateInterface extends IIndexable {
    advancedSearchParams: AdvancedSearchParams;
}

const initialState: StateInterface = {
    advancedSearchParams: {
        startDate: null,
        endDate: null,
        location: '',
        ageGroup: 0,
        carGroupId: 0
    }
}

const rootSlice = createSlice({
    name: 'root',
    initialState,
    reducers: {
        main(state: any, action: any) {
            return {
                ...state,
                ...action.payload,
            };
        }
    }
});

const dataStore = configureStore({
    reducer: rootSlice.reducer
});

export const rootActions = rootSlice.actions;
export default dataStore;

export type RootState = ReturnType<typeof dataStore.getState>;
export type AppDispatch = typeof dataStore.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
