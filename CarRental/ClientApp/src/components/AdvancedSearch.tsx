import React, { useState } from 'react';
import { TextField, Select, MenuItem, FormControl, InputLabel } from '@mui/material';
import { rootActions, useAppDispatch, useAppSelector } from '../store/DataStore';



const AdvancedSearch: React.FC = () => {

    const advancedSearchParams = useAppSelector(dataStore => dataStore.advancedSearchParams);
    const dispatch = useAppDispatch();

    const handleInputChange = (event: any) => {
        const { name, value } = event.target;
        const advancedSearchParamsUpdated: any = Object.assign({}, advancedSearchParams);
        advancedSearchParamsUpdated[name] = value;

        dispatch(
            rootActions.main({
                advancedSearchParams: advancedSearchParamsUpdated
            })
        );

    };

    return (
        <div className="advanced-search-container">
            <div >
                <TextField
                    label="Start Date"
                    type="date"
                    name="startDate"
                    
                    value={advancedSearchParams.startDate}
                    onChange={handleInputChange}
                    InputProps={{
                        className: 'search-input', 
                    }}
                    InputLabelProps={{
                        shrink: true,
                    }}
                />
                <TextField
                    label="End Date"
                    type="date"
                    name="endDate"
                    value={advancedSearchParams.endDate}
                    onChange={handleInputChange}
                    InputProps={{
                        className: 'search-input', 
                    }}
                    InputLabelProps={{
                        shrink: true,
                    }}
                />
                <TextField
                    label="Location"
                    name="location"
                    value={advancedSearchParams.location}
                    InputProps={{
                        className: 'search-input', 
                    }}
                    onChange={handleInputChange}
                />
                <FormControl>
                    <InputLabel className="group-select-label">Age Group</InputLabel>
                    <Select
                        name="ageGroup"
                        value={advancedSearchParams.ageGroup}
                        onChange={handleInputChange}
                        style={{ minWidth: "150px", marginRight:"10px" }}
                    >
                        <MenuItem value={0}>All</MenuItem>
                        <MenuItem value={1}>0-18</MenuItem>
                        <MenuItem value={2}>19-30</MenuItem>
                        <MenuItem value={3}>31 and above</MenuItem>
                    </Select>
                </FormControl>

            </div>
        </div>
        
    );
};

export default AdvancedSearch;
