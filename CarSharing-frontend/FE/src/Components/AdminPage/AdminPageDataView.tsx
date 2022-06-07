import React from 'react';

export interface IAdminPageDataView {
    id: number,
    data: any
}

function AdminPageDataView(props: IAdminPageDataView) {

    return (
        <div>
            {props.id} -- {props.data}
        </div>
    )

}

export default AdminPageDataView;
