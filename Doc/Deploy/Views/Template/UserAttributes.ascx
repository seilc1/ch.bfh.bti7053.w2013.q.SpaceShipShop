
<div class="profile-admin clearfix">
    <div>
        UserProfile:
        <input type="text" data-bind="value: CurrentUserId, valueUpdate: 'keyup'" />
    </div>

    <div>
        Edit:
        <input type="checkbox" data-bind="checked: Editable"/>
    </div>
</div>

<ul data-bind="foreach: UserProfileImage" class="profile-images">
    <li>
        <img data-bind="attr { src: Url }"/>
    </li>
</ul>

<ul data-bind="foreach: Categories" class="profile-infos clearfix">
    <li class="clearfix">
        <h2 data-bind="text: TextKey"></h2>
        
        <!-- ko foreach: Attributes -->

        <dl data-bind="if: (!$root.Editable() && Value()!='' && Value()!=null)">
            <dt data-bind="text: TextKey">
            </dt>
            <dd data-bind="text: Value"></dd>
        </dl>
        
        
        <dl data-bind="if: $root.Editable">
            <dt data-bind="text: TextKey">
            </dt>
            
            <input type="text" data-bind="value: Value, valueUpdate: 'keyup', event: {keyup: UpdateValue, blur: UpdateValueImmediate}"/>
        </dl>
        <!-- /ko -->
    </li>
</ul>